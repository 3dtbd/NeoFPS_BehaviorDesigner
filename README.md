# NeoFPS_BehaviorDesigner
NeoFPS and Behavior Designer integration assets

## Requirements
This repository was created using Unity 2018.4

It requires the assets [NeoFPS](https://assetstore.unity.com/packages/templates/systems/neofps-150179?aid=1011l58Ft) 
and [Behavior Designer](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277?aid=1011l58Ft).

## Installation

:::WARNING:::
At the current time this integration assumes that you have the [OPTIONAL] items below
installed. This should not create errors, but may create warnings. Features not requiring
optional components should still work. If you find this is not the case please raise an 
issue on GitHub.

This integration example is intended to be dropped in to a fresh project along with NeoFPS and Behavior Designer.

1. Import NeoFPS and apply the required Unity settings using the NeoFPS Settings Wizard. You can find more information about this process [here](https://docs.neofps.com/manual/neofps-installation.html).

2. Import the Behavior Designer asset.

3. Clone this repository to a folder inside the project Assets folder such as "NeoFPS_BehaviorDesigner"

4. There is currently a bug that means Global Veriables are not defined at import. To workaround this create a Behaviour Designer Global Variable of type GameObject with the name `NeoFPSPlayer`, see [Behaviour Designer documentation](https://opsive.com/support/documentation/behavior-designer/variables/global-variables/).

5. [Optional] If you want to play the demo scene with animations you need to import the following free animation pack: 
[Basic Motions FREE Pack](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271?aid=1101l866w)

6. [Optional] If you want to see how the integrations work with fully animated soldiers import the (not free, but cheap) 
[Toon Soldiers](https://assetstore.unity.com/packages/3d/characters/humanoids/toon-soldiers-52220?aid=1101l866w)
	
## Integration

This integration provides a number of Behavior Designer tasks that make it easier to integrate Neo FPS into your project. 
These can be located in the "Actions/Neo FPS" and "Conditional/Neo FPS" categories.

We've tried to make this as self-documenting as possible using the tooltips and documentation strings used 
in Behavior Designer itself. If you have questions ask them on the [Neo FPS Discord](https://discord.neofps.com/).

## Animation

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

## Demo Scenes

We've included a simple demo scene, see `NeoFPS_Melee_Ragdoll_Demo`. This requires the [Basic Motions Free Pack](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271?aid=1101l866w).
The goal of this is to show how to setup a character.
This scene shows simple movement using a NavMesh. The character will rush to the player and attack them (no animation for attack).
If you shoot the player they will die and then respawn a few seconds later.

There is also some demo scenes within the `Toon Soldier Scenes` folder. These require the (not free, but cheap) [Toon Soldiers](https://assetstore.unity.com/packages/3d/characters/humanoids/toon-soldiers-52220?aid=1101l866w).
Since these scenes have a number of enemies and animations they can show off more of what is possible with Behaviour Designer.

## Setting Up A new NPC Character with your own Animations

The following is a descripton of the basic steps for setting up your own model and animations to use the Behaviour Designer integrations.

### Character Setup

  1. Import your character into the scene
  2. Ensure that it has a collider 
  3. Add and configure the `AIController` script (this handles things like Death states and allows configuration of the cahracter).
  4. Ensure the NavMeshAgent is setup correctly (if the model didn't have one the simple locomotion agent script will add one)
  5. Add and configure Neo FPS `BasicHealthManager`
  6. Add and configure Neo FPS `BasicDamageHandler`
  7. Add and configure Neo FPS `SimpleSurface`
  8. Add an `AiBaseInventory` script and add available weapons to it (see `NeoFPS_BehaviourDesigner/Prefabs/Weapons)
  9. Add a Behaviour Tree component and drag `NeoFPS_BehaviourDesigner/Behaviour Trees/Basic Combat Behaviour` into the `External Behaviour` parameter
  
### Animations Setup

You can use any animation controller that can be driven by a `NavMeshAgent`. We provide one with the integration here that is driven by the example
behaviour trees in `NeoFPS_BehaviourDesigner/Behaviour Trees`. Note, at the time of writing there are a very limited range of animations provided, 
We need an animator, contributions are welcome. As noted above, you can download some free animations to provide core movement animations, but even
then we are still incomplete.

If you want to use our animation controller with the animations bundled then do the following:

  1. Add an Animator component to your model and use the `NeoFPS_BehaviourDesigner/Animations/Core Controller (Override This)` controller
  2. Add the `SimpleLocomotionAgent` script (this converts motion of the NavMeshAgent to input for the animation controller)
  
To use your own animations using out controller:

  1. Create an `Animator Override Controller` from `NeoFPS_BehaviourDesigner/Animations/Core Controller (Override This)`
  2. Configure each of the animators in this controller using your own animations

## Creating Weapons

The integrtation comes with some example weapons in `NeoFPS_BehaviourDesigner/Prefabs`. To create your own weapons for use by your NPC do the following:

  1. Ensure that your model has a root object that will be placed at Vector3.zero, that is the actual model should be a child
  2. Add the `AiSimpleWeapon` script and configure according to your desire
  3. Add the `FpsInventoryWieldable` script and configure it appropriately
  4. Create a prefab
  5. Add the prefab to the Backup Item slot of the inventory of the NPC in an appropriate demo scene
  6. Play the scene and hit pause
  7. Position the model (not the root object) such that it is correctly held by the character
  8. Record the models transform settings
  9. Stop the scene
  10. Apply the models transform settings to the prefab

To have an NPC use a weapon place it into the Inventory for that NPC.

(for now that is it, we need to add audio management and animation controll yet)