# VS Unity��LGSVLSimulator�ւ̓K�p

![Image0](Img/LGSVL/image0.jpg) 

1.IVehicleDynamics.cs��ҏW

![Image1](Img/LGSVL/image1.png) 

Rigidbody RB { get; }

�����݂��邪��L��CarSim�ł͑Ή��ł��Ȃ����߁ARigidBody�̎g�p���Ă��鑬�x�E�e���̑��x�x�N�g���A�p���x�x�N�g�����擾����悤�ɂ���B

����ɂ���L�ɍ��킹�ďC�����ׂ��_���C���B�C�����e�Ɋւ��Ă͊������܂��BRigidBody�̕ϐ����g�p���Ă�����̂�Interface�̂��̂Ɏg�p���邾���Ȃ̂ŊȒP�ł��B

2.LGSVL�V�~�����[�^�ɍŐV��CarSim�̃X�N���v�g��ǉ�

�ڍׂ̓`���[�g���A�����Q��

���̍ۂ�Samples��LGSVL�p�̃X�N���v�g������̂ł����Assets/Scripts/Dynamics/Examples�ɒǉ�

![Image2](Img/LGSVL/image2.png) 

3.�ԗ��ւ̓K�p
����Ή�������ԗ���Jaguar2015XE���߂����̂ł�����g�p����B

https://github.com/lgsvl/Jaguar2015XE

![Image3](Img/LGSVL/image3.png) 

Jaguar2015XE�̃v���n�u���_�u���N���b�N���C���B

![Image4](Img/LGSVL/image4.png) 

VehicleSMI�R���|�[�l���g���폜

![Image5](Img/LGSVL/image5.png) 

CarSimSMI��ǉ����̍ۂ�CarSimComponent���ǉ������B

![Image6](Img/LGSVL/image6.png) 


CarSimSMI�ɑ΂��ă^�C����Transform��ݒ肷��B

![Image7](Img/LGSVL/image7.png) 

CarSim��LGSVL�̃{�f�B�̌��_���قȂ�̂ŁACarSimComponent��BodyOffset��-1.5����͂���

![Image8](Img/LGSVL/image8.png) 

Ray�ɂ�锻���Default�݂̂ɐݒ肵�Ă��邪�A�g�p�󋵂ɍ��킹�Đݒ�B

![Image9](Img/LGSVL/image9.png) 

Jaguar2015XE��RigitBody�͉e���͎󂯂Ȃ��̂�IsKinematic���`�F�b�N����

![Image10](Img/LGSVL/image10.png) 

AssetBundles���r���h���A���s����B

![Image11](Img/LGSVL/image11.png) 

*���_�Ƃ��Ă͂܂�LGSVL��Physic Material�̖��C�W���̕ύX���킩�炸�A�X���b�v���Ă��܂��B


