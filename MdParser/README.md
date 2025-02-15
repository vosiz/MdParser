# MD parser
A simple MD file format parsing tool.

Make your own converters of MarkDown files.

## Usage
Just include MdParser namespace and Load, Parse and Save via Container class.

Here is example.
```csharp
using MdParser;

var parser = new MdParser.Container();
parser.Load(path_to_a_file);
parser.Parse(false); // true if not wanted to be strict
parser.Save(OutputType.TextFile, "output_path_file.txt"); // according to exported type
```

## Support
### MD components
- Text rows
- List (non-numeric) items
- Headlines/Headers - up to level 3

### Output formats
- SimpleText - txt file with text representation of MD file

### Testing and examples
- Added testing project to demonstrate usage

See simple convertion of MD file to txt.

```csharp
string TEST_RSC_1 = "../../TestResources/CHANGELOG.md";

try
{
    // create instance
    var parser = new MdParser.Container();

    // load MD file
    parser.Load(TEST_RSC_1);

    // process it
    parser.Parse(false);

    // save it to desired format
    parser.Save(OutputType.TextFile, "./Output/changelog.txt");
}
catch (Exception exc) {

    throw exc;
}
```