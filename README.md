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

### PubChem
1. Go [here](https://pubchem.ncbi.nlm.nih.gov/idexchange/idexchange.cgi) for the link to PubChem
2. For Input ID's, select the option 'Synonyms',
3. Click 'Choose File' and locate the **PubChemCompounds.txt** that was just created in the last step
4. Change Output ID's to 'SMILES'
5. Change the compression to 'No Compression'
6. Click Submit Job, and wait a few seconds

### PubChem to Biotransformer
1. Copy the compounds and smiles from the page on the last step,
2. Paste them into **Compounds_SMILES.txt** and Save
3. In Biotransformer_Assistant, move onto the step detailing the instructions for PubChem, it may not work if its empty
4. Press ENTER, Biotransformer_Assistant should output something on the lines of:
```
[Warning]: This may take several seconds, please be patient!
[Info]: PubChemOutput has: 120 possible compounds
[√]: Done! Press "ENTER" to continue
```
5. From there, you will be taken to the Biotransformer Arguments, input what is needed, it may look something like this:
```
[Info]: Enter metabolism type (default: allHuman) : allHuman

[Info]: Enter file type (default: csv) : csv

[Info]: Enter list name (default: list1) : list1
```
6. After you press enter, you are done! Everything will be sent to the Output Folder, and you will be set

## Parsing Skipped Compounds
**This section requires that you have completed Option 1 previously**
1. Navigate to **Output/Skipped_Compounds.txt**, it may look like this:
```
[4] PC(34:1)* (4)
[23] TG(20:5(5Z,8Z,11Z,14Z,17Z)/18:2(9Z,12Z)/22:5(7Z,10Z,13Z,16Z,19Z))[iso6] (23)
[24] TG(22:5(7Z,10Z,13Z,16Z,19Z)/16:0/22:5(7Z,10Z,13Z,16Z,19Z))[iso3] (24)
```
2. Once you figure out the SMILES compound, at the end of the line, place `|||` and then the SMILES
3. If done correctly, it should look like this as an example:
```
[4] PC(34:1)* (4) ||| smiles4
[23] TG(20:5(5Z,8Z,11Z,14Z,17Z)/18:2(9Z,12Z)/22:5(7Z,10Z,13Z,16Z,19Z))[iso6] (23) ||| smiles23
[24] TG(22:5(7Z,10Z,13Z,16Z,19Z)/16:0/22:5(7Z,10Z,13Z,16Z,19Z))[iso3] (24) ||| smiles24
```
4. Save and Exit the File
5. Go back to Biotransformer_Assistant, and select option 2 if you haven't already
6. You will be prompted to choose the arguments for Biotransformer
7. After you complete that, you're done! 
8. The Output will be located in **Output/SkippedSMILES_BiotransformerInput.txt**

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

## Known Bug
When entering settings, then exiting, then completeting steps 1 or 2, the program will get hung

## Errors & Troubleshooting
**Errors**

    "input.txt does not exist! Are you correctly following the documentation?"
    "PubChemOutput.txt does not exist, you must setup the program first!"
Both these errors result in either one of them not existing, you must setup the program first by selecting Option 3

**If any of your outputs are empty, and you are expecting a result, check your text files, one of them may be empty!**

