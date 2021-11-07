# Little-Clothes-Shop

## Code
All codes were written inside the testing time frame.

## Art
Tilemaps: https://opengameart.org/users/bluecarrot16

UI: https://opengameart.org/content/ui-pack

Character and Clothes: https://store.steampowered.com/app/1154430/RPG_Character_Builder/#:~:text=RPG%20Character%20Builder%20on%20Steam&text=An%20easy%20to%20use%20tool,for%20this%20software%20are%20positive.

## Map
The entire map was constructed during the testing time frame (Using the downloaded tilemaps)

## Audio
Urban bgs: https://www.youtube.com/watch?v=cAigX5rmlg4

Jazz BGM (Inside shop): https://www.bensound.com/royalty-free-music/jazz

Cashing: https://www.youtube.com/watch?v=4kVTqUxJYBA

Alarm: https://www.youtube.com/watch?v=hFIJaB6kVzk

Error sound: https://www.youtube.com/watch?v=Um39vxJdH6E

## My Experience

### The start
Basically, it started as a shock, I thought the task would be to create a game, like a game jam.

### First steps
When it started the first thing i did was to find some spritesheets, and they should be similar, so, i picked one artist at http://opemgameart.org and sticked with his arts

Then, I looked for any 2D character spritesheet creator, stumbled across a lot of variations of it until I landed at the one I credited, the spritesheet was simple, easy to create and had a lot of clothing options for me to create, so... I exported a male character, pants and LongsleevedShirt spritesheets and imported them to Unity

With the assets in hands, I created the animation for the naked player, and I thought "I must find a way of using the same animator for all clothes, without re-creating the animation for every single clothing item" after some while i finally had a code that overwrites the image with another using the same sprite name, this created a limitation that I had to work with: All spritesheets had to have similar array of sprites, with the same order (and names)

### The clothing import process
Import the spritesheet, rename it to "_", slice the spritesheet 8X4 Sprites, add another manual spriteslice at the (1,1) for the Icon, apply and rename the file to "Shirt" for example, now the name is shirt but all the sprites are "_0", "_1" and so on...

then I started creating the initial map (That was changed a lot until the last version

After I was kinda happy with the map, I made the Cash UI adding the Cash in/out animation and so on...
Then, I created an UI for the shopping cart and current shop.

After having the simple UI kinda working, I started on creating the Inventory system

## The inventory System
The inventory system works by passing a simple object inheriting the "InventoryItem" class, this is the same object list present in all types of inventory through the game
The inventoryTypes are:
- Personal Inventory: Displays all items the player currently have (Q)
- Shop Inventory: Displays all items the current shop have (in the game there are multiple isles with different items in it)
- Shopping Cart: The items the player added to it
- Owned Clothes Inventory: When entering the Dresser it filters all items allowing just clothing to pass and then filters it again removing all clothes the player can't wear (for example, female clothes on male and vice versa) <This limitation was added because the spritesheet I used had REALLY different body types.
- Style Inventory: All the clothing the player currently wears

## Some visual changes
Then I returned to the map and thought "this map needs some depth" then i started adding elements trying to make it more organic, adding different types of rocks in random places and so on, then I created the system that hides the building outside tilemaps so you could see the inside (instead of just teleporting the player inside)

then, I had to create an easy way of creating and loading all the JSON files, after some time the JsonInventoryHandler was born.

all shops read a JSON file with all items it needs to load to create it's inventory list.
  
## Changing clothes
  After everything was ready, I started tinkering with the apparel system, thas allowed the player to spawn clothes from inventory and/or taking off some clothes back to the inventory.
  
## Random idea
  After I looked the game kinda complete I thought "What if the player leaves the shop without paying?"
  So I created the alarm system and the stealing action
  
Then I started to polish some design issues I had, fixing some bugs and changing a little bit the tilemaps to the final form
