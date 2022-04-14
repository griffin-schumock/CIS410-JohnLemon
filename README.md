# CIS410-Assignment2
JohnLemon Unity Game

Authors: Griffin Schumock and Austin Mello

Split up the main tutorial between authors.

ADDED GAMEPLAY ELEMENTS
=======================
Dot Product (Austin) - I implemented the facing function from the demo in last 
week's lecture into the gargoyle enemies. I did this by following the example 
code from our canvas page, creating a new script named LookAtPlayer, and adding
the script to the gargoyle prefab.  If you watch the model, its kind of hard to
see the rotation.  But if you watch the cone from the gargoyle's flashlight,
it's obvious enough.

Linear Interpolation (Austin) - I used linear interpolation by adding to the 
script for the wall lights called LightFlicker.  This was pretty tricky, 
because the script has this weird mode feature that I had never seen before.
In any case, the light(s) will dim/brighten as the player's proximity to the 
light(s) increases/decreases.  The distance was found by creating a 
DistToPlayer() function (line 96), which was called by the 
ChangeLERPFlickerLightIntensity() function (line 72).  The latter function is 
what hold the linear interpolation algorithm.  See code comments for details.
    In order to better see the difference, as well as to give the player a 
helpful hint, I only implemented the LERP function on the lights that will lead
the player from the spawn point to the finish line.  The dimming effect is most 
obvious if you watch the glow effect around the model.
    	IMPORTANT: Make sure the Light->Inspector->Light Flicker (Script)->Flicker
Mode setting is set to LERP fromt he dropdown menu!  Otherwise, the effect 
will be the default one! Modes are kinda cool, as a side note.


Particle Effect (Griffin) - Added a rain particle system that activates when the player (JohnLemon)
                  enters the bathroom in the starting area with the ghost in the shower.
                  It is triggered on while the player is inside the room and will stop when
                  the player exits it via a trigger that is the size of the room.
