# Applying VS Unity to LGSVLSimulator

![Image0](Img/LGSVL/image0.jpg) 


1.Edit IVehicleDynamics.cs

![Image1](Img/LGSVL/image1.png) 

Rigidbody RB { get; }

Since the above cannot be supported by CarSim, the velocity and angular velocity vectors of each axis and the velocity vector of each axis used by RigidBody are to be obtained.

This fixes what needs to be fixed to match the above. I won't bore you with the details of the fixes, but I'll tell you that I'm going to use the RigidBody variables in It's easy to use for the Interface stuff.

2.The latest CarSim script added to the LGSVL simulator

See the tutorial for details.

At that time, there is a script for LGSVL in Samples, so add it to Assets/Scripts/Dynamics/Examples

![Image2](Img/LGSVL/image2.png) 

3.Application to vehicles
The vehicle to be supported this time is the Jaguar 2015XE, which is close enough to be used.

https://github.com/lgsvl/Jaguar2015XE

![Image3](Img/LGSVL/image3.png) 

Double click on the Jaguar 2015XE prefab to fix it.

![Image4](Img/LGSVL/image4.png) 

Removing the VehicleSMI component

![Image5](Img/LGSVL/image5.png) 

CarSimSMI is added, and CarSimComponent is added as well.

![Image6](Img/LGSVL/image6.png) 


Set the Transform of the tire to CarSimSMI.

![Image7](Img/LGSVL/image7.png) 

Since the origin of the CarSim and LGSVL bodies are different, the CarSimComponent has a Enter a BodyOffset of -1.5

![Image8](Img/LGSVL/image8.png) 

Judgment by Ray is set only by default, but it is set according to the situation of use.

![Image9](Img/LGSVL/image9.png) 

Jaguar2015XE's RigitBody is unaffected, so check IsKinematic

![Image10](Img/LGSVL/image10.png) 

Build and run AssetBundles.

![Image11](Img/LGSVL/image11.png) 

*The problem is that I still don't know how to change the coefficient of friction for Physic Material in LGSVL and it slips.


