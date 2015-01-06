SampleCode
==========
This is a little sample code I wrote. The program reads from a text file and loads it into an “Image” object.

#Design 
- I generally try to follow the S.O.L.I.D principles when I write code, and make sure that I keep good separation of concern. I generally always inject interfaces and avoid coupling to concrete implementations.

- For testing I didn't go to in depth and stayed with a sort of BDD style. Using Given/When/Then's think cucumber/specflow red/green/refactor ect.

- I kept the original Image class the same with the exception of changing the Data field to a 2d byte array instead of a 2d int. Since the entries will never go over 255 it seemed pointless to store them as ints. It also gave me a free value check to make sure I wasn't storing any values over 255 dues to a bad data file or somthing.

#Notes 
- If this wasnt simply a sample I would write more tests around validating that the program can handle a reasonable amount of errors in formatting. Like leading or trailing spaces or quammas that have white space around them ect. The depth of which would depend on the requirements. 

- This implementation will not handle large files because I simply just read until EOF. This could be easly fixed by reading the file line by line or in small blocks, and using somthing like yield to make sure that your not drinking from a fire hose.

- I assumed that there is only one data set per file. It would make sense that you would possibly want multiple images in one file separated by a delimiter or somthing. But for this implementation I assumed one image per file.- The easiest way to trace through the program is to run/look at the AClientCanGetAImageFromAImageFile() method in the ImageLoaderTestData class.  