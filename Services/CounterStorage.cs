using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Counter.Models;

namespace Counter.Services
{
    public static class CounterStorage
    {
        private static string DataFile =
            System.IO.Path.Combine(FileSystem.AppDataDirectory, "CounterData.json");

        public static List<CounterData> Load()
        {
            if (!File.Exists(DataFile))
                return new List<CounterData>();

            var json = File.ReadAllText(DataFile);
            return JsonSerializer.Deserialize<List<CounterData>>(json)
                   ?? new List<CounterData>();
        }

        public static void Save(List<CounterData> counters)
        {
            var json = JsonSerializer.Serialize(counters);
            File.WriteAllText(DataFile, json);
        }
    }
}
