using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AddonObfuscator.Core
{
    public class Obfuscator
    {
        private readonly string source;
        private readonly string target;
        private readonly Formatting formatting;

        public Obfuscator(string source, string target, Formatting formatting = Formatting.Default)
        {
            this.source = source;
            this.target = target;
            this.formatting = formatting;
        }

        public void Run()
        {
            foreach (var filePath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                var directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(directory))
                    return;

                var newDirectory = Path.Combine(target, Path.GetRelativePath(source, directory));
                Directory.CreateDirectory(newDirectory);

                var newFilePath = Path.Combine(newDirectory, Path.GetFileName(filePath));

                if (Path.GetExtension(newFilePath) == ".json" && Path.GetFileNameWithoutExtension(newFilePath) != "manifest")
                    File.WriteAllText(newFilePath, Obfuscate(ApplyFormatting(File.ReadAllText(filePath))));
                else
                    File.WriteAllText(newFilePath, File.ReadAllText(filePath));
            }
        }

        private string Obfuscate(string content)
        {
            return Regex.Replace(content, "(\"(?:.*?)\")", (match) =>
            {
                var stringBuilder = new StringBuilder();
                var escape = false;

                foreach (var character in match.Value)
                {
                    stringBuilder.Append(character != '"' || escape ? $"\\u{(ushort)character:X4}" : '"');
                    escape = character == '\\';
                }

                return stringBuilder.ToString();
            }, RegexOptions.Compiled);
        }

        private string ApplyFormatting(string content)
        {
            switch (formatting)
            {
                case Formatting.Minify:
                    {
                        var options = new JsonSerializerOptions()
                        {
                            ReadCommentHandling = JsonCommentHandling.Skip,
                            WriteIndented = false
                        };

                        var json = JsonSerializer.Deserialize<object>(content, options);
                        return JsonSerializer.Serialize(json, options);
                    }

                default:
                    return content;
            }
        }
    }
}
