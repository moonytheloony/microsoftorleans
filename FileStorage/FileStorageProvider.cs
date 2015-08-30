using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage
{
    using System.IO;

    using Newtonsoft.Json;

    using Orleans;
    using Orleans.Runtime;
    using Orleans.Storage;

    /// <summary>
    /// Class FileStorageProvider.
    /// </summary>
    public class FileStorageProvider : IStorageProvider
    {
        public Task ClearStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, Orleans.IGrainState grainState)
        {
            var fileInfo = this.GetFileInfo(grainType, grainReference);
            fileInfo.Delete();
            return TaskDone.Done;
        }

        public Task Close()
        {
            return TaskDone.Done;
        }

        public Orleans.Runtime.Logger Log { get; set; }

        public async Task ReadStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, Orleans.IGrainState grainState)
        {
            var fileInfo = this.GetFileInfo(grainType, grainReference);
            if (!fileInfo.Exists)
            {
                return;
            }

            using (var stream = fileInfo.OpenText())
            {
                var json = await stream.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                grainState.SetAll(data);
            }
        }

        public Task WriteStateAsync(string grainType, Orleans.Runtime.GrainReference grainReference, Orleans.IGrainState grainState)
        {
            var json = JsonConvert.SerializeObject(grainState.AsDictionary());
            var fileInfo = this.GetFileInfo(grainType, grainReference);
            using (var stream = fileInfo.OpenWrite())
            {
                using (var writer = new StreamWriter(stream))
                {
                    return writer.WriteAsync(json);
                }
            }
        }

        // helper fnction created.
        FileInfo GetFileInfo(string grainType, GrainReference grainReference)
        {
            return new FileInfo(Path.Combine(string.Format(@"{0}\{1}-{2}.json", this.directory, grainType, grainReference.ToKeyString())));
        }

        public Task Init(string name, Orleans.Providers.IProviderRuntime providerRuntime, Orleans.Providers.IProviderConfiguration config)
        {
            //// prepare provider
            this.Name = name;
            this.directory = config.Properties["directory"];
            return TaskDone.Done;
        }

        private string directory;
        public string Name { get; set; }
    }
}
