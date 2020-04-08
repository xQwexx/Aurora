using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aurora;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Device_SteelSeries
{
    public partial class SteelSeriesDevice
    {
        private HttpClient client;
        private JObject baseObject = new JObject();
        private JObject baseColorObject = new JObject {{"Event", "AURORA"}, {"data", new JObject()}};
        private Task pingTask;
        private CancellationTokenSource pingTaskTokenSource = new CancellationTokenSource();
        private bool loadedLisp;
        private JObject dataColorObject => (JObject)baseColorObject["data"];

        private void sendLispCode()
        {
            try
            {
                var core = (JObject)baseObject.DeepClone();
                core.Add("game_display_name", "Project Aurora");
                core.Add("icon_color_id", 0);
                sendJson("/game_metadata", core);
                core.Remove("game_display_name");
                core.Remove("icon_color_id");
                using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Device_SteelSeries.GoCode.lsp")))
                {
                    core.Add("golisp", reader.ReadToEnd());
                }
                sendJson("/load_golisp_handlers", core);
                pingTask = Task.Run(async () => await sendPing(pingTaskTokenSource.Token), pingTaskTokenSource.Token);
                loadedLisp = true;
            }
            catch (Exception e)
            {
                Global.logger.Error(e,"SteelSeries Lisp Code failed.");
                throw;
            }
        }

        private void loadCoreProps()
        {
            var file = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SteelSeries/SteelSeries Engine 3/coreProps.json"));
            if (!file.Exists)
                throw new FileNotFoundException($"Core Props file could not be found.");

            try
            {
                var reader = file.OpenText();
                var coreProps = JObject.Parse(reader.ReadToEnd());
                reader.Dispose();
                client.BaseAddress = new Uri("http://" + coreProps["address"]);
                sendLispCode();
            }
            catch (Exception e)
            {
                Global.logger.Error(e, "SteelSeries Core Props Load failed.");
            }
        }

        private void setKeyboardLed(byte led, Color color)
        {
            if(!(dataColorObject).ContainsKey("keyboard"))
                dataColorObject.Add("keyboard", new JObject {{"hids", new JArray()}, {"colors", new JArray()}});
            ((JArray) dataColorObject["keyboard"]["hids"]).Add(led);
            ((JArray) dataColorObject["keyboard"]["colors"]).Add(new JArray{color.R, color.G, color.B});
        }

        private void setOneZone(Color color)
        {
            dataColorObject.Add("onezone", new JObject{{"color",new JArray {color.R, color.G, color.B}}});
        }

        private void setTwoZone(Color[] colors)
        {
            dataColorObject.Add("twozone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setThreeZone(Color[] colors)
        {
            dataColorObject.Add("threezone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setFourZone(Color[] colors)
        {
            dataColorObject.Add("fourzone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setFiveZone(Color[] colors)
        {
            dataColorObject.Add("fivezone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setEightZone(Color[] colors)
        {
            dataColorObject.Add("eightzone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setTwelveZone(Color[] colors)
        {
            dataColorObject.Add("twelvezone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setSeventeenZone(Color[] colors)
        {
            dataColorObject.Add("seventeenzone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setTwentyFourZone(Color[] colors)
        {
            dataColorObject.Add("twentyfourzone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setHundredThreeZone(Color[] colors)
        {
            dataColorObject.Add("hundredthreezone", new JObject{{"colors", colorToJson(colors)}});
        }

        private void setLogo(Color color)
        {
            dataColorObject.Add("logo", new JObject{{"color",new JArray {color.R, color.G, color.B}}});
        }

        private void setWheel(Color color)
        {
            dataColorObject.Add("wheel", new JObject{{"color",new JArray {color.R, color.G, color.B}}});
        }

        private void setMouse(Color color)
        {
            dataColorObject.Add("mouse", new JObject{{"color",new JArray {color.R, color.G, color.B}}});
        }

        private void setGeneric(Color color)
        {
            dataColorObject.Add("periph", new JObject{{"color",new JArray {color.R, color.G, color.B}}});
        }

        private async Task sendPing(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    await sendJsonAsync("/game_heartbeat", baseObject);
                }
                catch (Exception e)
                {
                    Global.logger.Error(e, "Error while sending heartbeat to SteelSeries Engine trying to restart.");
                    //To stop all other events from erroring and try and reboot steelseries engine
                    Reset();
                }
            }
        }

        private void sendLighting()
        {
            sendJson("/game_event", baseColorObject);
        }

        private void sendJson(string endpoint, object obj)
        {
            var t = sendJsonAsync(endpoint, obj).GetAwaiter().GetResult();
        }

        private Task<HttpResponseMessage> sendJsonAsync(string endpoint, object obj)
        {
            return client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
        }

        private static JArray colorToJson(Color[] colors)
        {
            return new JArray(colors.Select(color => new JArray {color.R, color.G, color.B}).ToArray());
        }
    }
}