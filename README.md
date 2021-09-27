# Biotransformer-Assistant
BioTransformer-Assistant is a project made for the sole purpose of easily converting compounds into parsed down data, that can be inputted into different programs

# Requirements
This program requires .NET Core Runtime, you can download it here : [Download](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-3.1.19-windows-x64-installer)

## Setting Up & Starting

**You must extract Biotransformer_Assistant into a folder, it will not work without it**

1. Make sure you have .NET Core 3.1 Installed
2. Run Biotransformer_Assistant and select option 3 first, this will setup the program

### Parsing Compounds for PubChem
PubChem is very picky with how it accepts compounds, with this being said, you will need to parse them

This can be done by selecting the first option, "Load compounds from file"

1. Copy the list of compounds from a spreadsheet or where-ever you have them
2. Find **RawCompounds.txt**, it should be in the same folder that the executeable is in
3. Paste the compounds, make sure that each compound has its own line, it will not work otherwise
4. Press Ctrl+S to save the file then close
5. Continue with the program
6. When it has completed parsing the Compounds, navigate to the Output Folder in the Biotransformer_Assistant folder.
7. Find **PubChemCompounds.txt**, and in there will be the parsed compounds ready to be put into PubChem

## File Structure
```
- Biotransformer_Assistant.deps
- Biotransformer_Assistant.dll
- Biotransformer_Assistant.exe
- Biotransformer_Assistant.pdb
- Biotransformer_Assistant.runtimeconfig.dev
- Biotransformer_Assistant.runtimeconfig
- Compound_SMILES.txt
- RawCompounds.txt
- Output
      - Biotransformer_Input.txt
      - PubChemCompounds.txt
      - SMILES.SpreadsheetKey.txt
```

## Errors & Troubleshooting
**Errors**

    "input.txt does not exist! Are you correctly following the documentation?"
    "PubChemOutput.txt does not exist, you must setup the program first!"
Both these errors result in either one of them not existing, you must setup the program first by selecting Option 3

**If any of your outputs are empty, and you are expecting a result, check your text files, one of them may be empty!**

