import System
import System.Xml
import System.Text.RegularExpressions
import System.IO

#
# Various support functions for build processes.
# Mark Embling
# Latest at https://gist.github.com/markembling/672e4ce804db7f2da2df
#

# Update the given XML file, replacing the value at the provided 
# XPath location with the given value.
def xmlpoke(fileName as string, xpath as string, value as string):
    document = XmlDocument()
    document.Load(fileName)
    document.DocumentElement.SelectSingleNode(xpath).Value = value
    document.Save(fileName)

# Read the value of the given environment variable.
def read_env_var(name):
    return Environment.GetEnvironmentVariable(name)

# Read the version number from the given AssemblyInfo.cs file. 
# Note: provides the AssemblyVersion, not AssemblyFileVersion.
# The 'segments' argument gives the number of parts to return:
#  1 => 1; 2 => 1.2; 3 => 1.2.1; 4 => 1.2.1.44; 0 => all
def read_version_number(assembly_info_path, segments as int):
    contents = File.ReadAllText(assembly_info_path)
    re = Regex("\\[assembly: AssemblyVersion\\(\"(?<version>[0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]?)\"\\)\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled)
    re_match = re.Match(contents)
    version = re_match.Groups["version"].Value
    parts = version.Split(char('.'))
    if segments == 0:
        return version
    else:
        result = ""
        for i in range(0, segments):
            if i > 0: result += "."
            result += parts[i]
        return result

# Overload of the above which returns the full version number and takes 
# no second argument specifying the number of segments.
def read_version_number(assembly_info_path):
    return read_version_number(assembly_info_path, 0)

# Update the build number (3rd part) in the given AssemblyInfo.cs file.
# Note: updates both the AssemblyVersion AND the AssemblyFileVersion.
def update_build_number(assembly_info_path, build_num):
    contents = File.ReadAllText(assembly_info_path)
    re = Regex("(?<firstpart>\\[assembly: Assembly(File)?Version\\(\"[0-9]?\\.[0-9]?\\.)[0-9]?(?<lastpart>\\.[0-9]?\"\\)\\])", RegexOptions.IgnoreCase | RegexOptions.Compiled)
    replacement = "\${firstpart}" + build_num + "\${lastpart}"
    contents = re.Replace(contents, replacement)
    File.WriteAllText(assembly_info_path, contents)

# Read the NuGet API key from my DropBox for publishing NuGet packages.
def read_nuget_api_key():
  home_dir = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%")
  key_path = home_dir + "/Dropbox/nuget-api-key.txt"
  if File.Exists(key_path):
    with input = File.OpenText(key_path):
      return input.ReadLine()
  else:
    raise Exception("NuGet API key not present. Are you Mark Embling?")

# Provide a yes/no prompt for confirmation.
def prompt(text):
  Console.ForegroundColor = ConsoleColor.Green
  Console.Write("${text} (y/n) ")
  Console.ResetColor();
  answer = Console.ReadKey()
  Console.WriteLine()
  return answer.Key.ToString() == 'Y' or answer.Key.ToString() == 'y'

# Creates an update.xml at the given path, with the provided attributes.
def create_update_xml(file_path as string, version, 
                      release_date as date, product_url as string,
                      packages):
    writer = XmlTextWriter(file_path, null)
    writer.Formatting = Formatting.Indented
    writer.WriteStartDocument()

    # app (root) element
    writer.WriteStartElement("app")
    writer.WriteAttributeString("xmlns", "http://markembling.info/xmlschema/appupdates/1")
    
    # version element
    writer.WriteStartElement("version")
    writer.WriteString(version)
    writer.WriteEndElement()

    # releasedate element
    writer.WriteStartElement("releasedate")
    writer.WriteString(release_date.ToString("yyyy-MM-dd"))
    writer.WriteEndElement()

    # producturl element
    writer.WriteStartElement("producturl")
    writer.WriteString(product_url)
    writer.WriteEndElement()

    for package as Hash in packages:
        writer.WriteStartElement("package")

        if package['autoupdate']:
            writer.WriteAttributeString("autoupdate", "true")

        # url element
        writer.WriteStartElement("url")
        writer.WriteString(package['url'])
        writer.WriteEndElement()

        # type element
        writer.WriteStartElement("type")
        writer.WriteString(package['type'])
        writer.WriteEndElement()

        writer.WriteEndElement()

    # Close root element
    writer.WriteEndElement()

    # Write out to disk
    writer.Flush()
    writer.Close()
