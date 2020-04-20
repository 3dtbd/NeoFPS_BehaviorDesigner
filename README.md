# NeoFPS_BehaviorDesigner
NeoFPS and Behavior Designer integration assets

## Requirements
This repository was created using Unity 2018.4

It requires the assets [NeoFPS](https://assetstore.unity.com/packages/templates/systems/neofps-150179?aid=1011l58Ft) 
and [Behavior Designer](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277?aid=1011l58Ft).

## Installation

This integration example is intended to be dropped in to a fresh project along with NeoFPS and Behavior Designer.

1. Import NeoFPS and apply the required Unity settings using the NeoFPS Settings Wizard. You can find more information about this process [here](https://docs.neofps.com/manual/neofps-installation.html).

2. Import the Behavior Designer asset.

3. Clone this repository to a folder inside the project Assets folder such as "NeoFPS_BehaviorDesigner"

4. [Optional] If you want to play the demo scene you need to import [Basic Motions Free Pack](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271?aid=1101l866w) 
	
## Integration

This integration provides a number of Behavior Designer tasks that make it easier to integrate Neo FPS into your project. 
These can be located in the "Actions/Neo FPS" and "Conditional/Neo FPS" categories.

We've tried to make this as self-documenting as possible using the tooltips and documentation strings used 
in Behavior Designer itself. If you have questions ask them on the [Neo FPS Discord](https://discord.neofps.com/).

## Animations

Included in the Scripts folder is a class to translate Nav Mesh Agent movements to animation inputs, see 
`SimpleLocomotionAgent`. This monobehaviour can be dropped on your NPCs and it will convert movement into 
inputs for your Animator. By default the inputs are:

  * `Move` a boolean indicating if the agent is moving under its own power
  * 'XVelocity` a relative velocity between 0 and 1 on the x axis
  * 'YVelocity` a relative velocity between 0 and 1 on the y axis

The names of these parameters can be changed in the inspector.

By setting up your animation controller to use these inputs you can have your character animate appropriately
based on the movements defined in your behaviour tree. The sample scene contains a character that is
wired up to use this method. However, you will need the 
[Basic Motions Free Pack](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271?aid=1101l866w) 
for the animations to work. Install this pack.

You can, of course, trigger animations from directly within your Behaviour Trees, or you can integrate your
favorite controller. 

## Demo Scene

We've included a simple demo scene, see `NeoFPS_BehaviorDesigner_Demo`