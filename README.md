# N64-PPL-Editor

ppl editor is an editor for viewing, extracting and editing the pokemon puzzle league rom (on N64, any version (French, German, Europe and USA)).

Currently, it allows :
- to extract textures (compressed or not)
- replace or/and modify compressed textures (without size or format limitation)
- modify animations on compressed textures
- replace* uncompressed textures (but strictly respecting the format of the image)
- modify the scenes of the game (text, textures, location of textures, management of the font (size and color) and some additional options)
- modification of the movies (replacing or removing)
- modification of audio
- possibility to extend the rom to 64Mb

*french version has much more uncompressed texture available than others.

It is also possible to view the gamecube version from nintendo puzzle collection.

## How To use it

you can download directly the binary version of the release.
You can try the portable version, if it didn't work, use the install version (who install needed requirements like net framwork, etc...)

for those who want to compile by the sources, you can use visual studio 2019 (no specific references/requirements, .net framwork 4.7.2 used)

use 3 dots button "..." and select your rom (or ISO if it's gamecube version) then button "load rom"
for N64 version, beware only .z64 is accepted (you can find tools on internet for converting them to z64)

### compressed texture part (first tab)

it works as follows you have containers display in the list. If you expend it, you can see texture(s) inside.
Each container can hold one or more textures.

If you had more than one texture, you can change the txture display time for each texture. If you select the container that hold texture and right click on it, you can change container type "fixed", "animated", or "texture scroll".

you also have ability to extract them all if you want.

you can right click on textures and suppress them (or by clicking on "suppr" keyboard button)
you can add some textures (multiple texture insertion is allowed), it will be automatically compressed.

### uncompressed texture part (second tab)

on this tab, you can extract all textures available and replace them.
/!\ Beware, you have to respect the format (check the number of colors, check if there transparency, etc...)
if you dont respect it, the tools will probably erase some textures data and modified rom probably won't launch (or not as expected)

The french version has all uncompressed textures available in the rom,
Others version have just one or two textures, because this work is repetitive and tiresome.
For adding new, please feel free to contact me (actually i just need their location, witch is visible with tools like texture64)

### scene management part (third tab)

In this part you can modify the comportement of the scene.

for starting, you have "scene folder" witch contains several scenes.
each scene folder can be extracted and loaded by right clicking on one of them.

On each scene, you have preview on all compressed textures (textures uncompressed is not available for modifying)

On each scene (text part) you can change :
- the selectionned text and edit it.
- add new text
    - in same scene : used for display multiple text at the same time
    - in new scene : display "one by one" each text (like discuss with oponnent)
- remove text
- change font color, size and position
    - beware, thoses who had text effect "centered" enabled, you can't change their positions (unless if you uncross this text effect)
- text effects, not sure how accurate these options are.. 
    - scrolling : display the text one character at a time
    - Centered : weird options not fully decoded.
    - Hidden : if some text is hidden in the game
    - Wait input : generally wait input for display next text
    - Has font color : enable this option to have color on your text
    - Extra 3 : not very usefull, you can define personalized spaces between each character (used when alphabet is displayed in "enter name" scene)
    - others is not fully decoded.

On each scene (texture part) you can :
- select which texture should be modified
- change his position
- replace a texture by another (who is present in the same "scene folder")
- add a texture in the "scene folder"

### movie part (fourth tab)

please follow explanation on the specified tab. more information will be probably delivered later for best describing modification.

### Audio part (fifth tab)

no explanation for now, i will write specific document, because it has few requirements and technics things. (not veryy complicated, but take huge time)
### Button Modify ROM part

This button is for write to the rom the content that you modify.
/!\ beware, it will modify the initial rom that you define (so be sure to have a copy)

If you not modify the rom, all the changes will be lost when quit the tools.

## F.A.Q.

Q : my compressed texture is weirdly displayed
A : Please try to change the texture width and heigth by a multiple of 4 and ensure the texture width and heigth is always pair (ex. 64px, 68px, and so on)
