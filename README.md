# Biotransformer-Assistant
BioTransformer-Assistant is a project made for the sole purpose of easily converting compounds into parsed down data, that can be inputted into different programs

## Setting Up
This project has moved over to **.NET Core 3.1** rather than **.NET Framework 4.8**

**Starting Off:**
1. Open BTA.zip and extract to anywhere on your computer
2. Launch the .EXE file, the name of the file may vary on which download you use
3. Upon opening the program, you should be greeted with a console that looks like this:

```
BioTransformer-Assistant

1. Paste compound individually (Good for quick and easy conversions)
2. Load Compounds via File (If you got a lot of compounds you want done quickly)
3. Setup Program (Only use if this is your first time)

Input:
```
4. Select Option 3, this will create all the needed files for the later steps of the program
5. Close and Re-Open the program when the files are created
6. From here, please go to the **Using BTA** section of the README

## Using BTA (BioTransformer-Assistant) (Option 1)
Using BTA is fairly straight forward and easy to do as long as you follow the instructions,
1. Due to how BTA has been created, you are able to copy and paste entire compounds straight into the program, and it will process them the same
2. As an example, we will be using these compounds for the example:
```
L-Palmitoylcarnitine* +1.6730405
cis-5-Tetradecenoylcarnitine
DG(22:6(4Z,7Z,10Z,13Z,16Z,19Z)/22:2(13Z,16Z)/0:0) +6.5375094
DG(22:6(4Z,7Z,10Z,13Z,16Z,19Z)/22:4(7Z,10Z,13Z,16Z)/0:0) +6.2044625
DG(22:6(4Z,7Z,10Z,13Z,16Z,19Z)/22:4(7Z,10Z,13Z,16Z)/0:0) +6.2780676
PC(34:1)* +5.71154
```
3. Selecting Option 1, you will be met with instructions telling you to enter in the compounds
4. Copy the example above, and right click the mouse to paste them into the program

      4a. If the string you are copying is not formatted correctly, move to **Using BTA (Option 2)**
  
6. When you are done entering them in, type **STOP** into the program, at it will move forward.
7. It will then show you all of the parsed compounds, then take those compounds and copy them to the clipboard
8. After pressing the Enter key and moving forward, you will be prompted with instructions for PubChem
9. Take the output of PubChem and find the file labeled PubChemOutput, open it and paste the results into it. Save and Exit
10. Head back into BTA, and press Enter. This will process the text inside of PubChemOutput
11. From here, you must follow the steps for what you want the BioTransformer Input to look like, pressing enter if the input is empty, will enter whatever is defaulted
12. After all of that, it will take you to one final screen that displays the Biotransformer Input

However, if you would like to view the SMILES Key, and the Input, find the BioTransformerInput text file and open it

This will contain all the information that has been generated

## Using BTA (Option 2)
Option 2 is more oreintated for larger use, or if your source is not formatted correctly

Either way, most of the steps are the same
1. You must start in the folder which you extracted from, find the file labeled 'input'
2. Take your unparsed compounds, as example the block from Option 1. And paste it into the text file, save it and exit
3. Head back into the program, from here you are clear to press '2'
4. It will immediately parse all the data inside the file, and give it back to you 
5+. All Steps beyond this are exactly the same as Option 1

## Errors & Troubleshooting
**Errors**

    "input.txt does not exist! Are you correctly following the documentation?"
    "PubChemOutput.txt does not exist, you must setup the program first!"
Both these errors result in either one of them not existing, you must setup the program first by selecting Option 3

**If any of your outputs are empty, and you are expecting a result, check your text files, one of them may be empty!**

### Unable to Open Program
You may need to install the .NET Core Runtime

Dotnet Core Runtime - [Download](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-3.1.19-windows-x64-installer)

