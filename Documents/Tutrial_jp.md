# VS Unity�`���[�g���A��

1.VsDotnet�v���W�F�N�g��Clone���_�E�����[�h���Ă�������

2.Unity2019(�����ł�2019.1.13f1)�Ńe���v���[�g�RD��I�т܂��B�v���W�F�N�g���͂����ł�VsUnity Tutorial�Ƃ��܂��B
![Image1](Img/Tutorial/image1.png) 

3�DFile���j���[->Build Settings->Player Settings�������܂��B
Player�� Configuraion�J�e�S����Api Compatiblity Level��.NET4.x�ɂ��܂��B
Allow �eunsafe�f Code�Ƀ`�F�b�N�����܂��B

![Image2](Img/Tutorial/image2.png) 
 
4�DClone����VsUnityTutorial��Assets/StreamingAssets�����Assets/Scripts�t�H���_��V�K�쐬����VsUnityTutorial��Assets�փR�s�[���܂��B

![Image3](Img/Tutorial/image3.png) 
![Image4](Img/Tutorial/image4.png) 

Unity�ɖ߂����ۂ�Console�ɃG���[���o�Ȃ����Ƃ��m�F���Ă�������

![Image5](Img/Tutorial/image5.png) 
 
5�D�܂��n�ʂ��쐬���܂�
Hierachy�ŉE�N���b�NGameObject->3D Object->Plane��I�����܂�

![Image6](Img/Tutorial/image6.png) 
 
�ǉ������n�ʂɑ΂��āAPosition��0�C0�C0�AScale��100,100,100�ɂ��܂��B�����ł͒n�ʂɒ��F�̃}�e���A����ݒ肵�Ă��܂����A�K�X�ݒ肵�Ă��������B

![Image7](Img/Tutorial/image7.png) 
 

6�D�ԗ��̃x�[�X�ƂȂ�GameObject��ǉ����܂��B�n�ʂƓ��l��Hierachy�ŉE�N���b�N��GameObject->Create Empty��I�����܂��B�ǉ�����GameObject��Vehicle�Ɛ���t���܂��B���̈ʒu��0,0,0�ɂ��Ă����܂��B

![Image8](Img/Tutorial/image8.png) 
 
7�D�ǉ�����Vehicle��Hierachy�ŉE�N���b�N���AGameObject->Cube��I�����܂��B

![Image9](Img/Tutorial/image9.png) 

�Ԃ̎ԗ��{�̂ŁA�����ł�Position0,0.8,-1.33,Scale,1.4,1,4�ɐݒ肵�܂��B���m�ɂ�CarSim�̐ݒ�̍��W�n�ɍ����p�Ɉʒu�E�T�C�Y�̒������s���܂��B

![Image10](Img/Tutorial/image10.png) 

�܂�BoxColider�̃`�F�b�N���O�������ɂ��܂��B(CarSim�͏Փ˂ȂǂɑΉ����Ă��Ȃ�����)

![Image10_2](Img/Tutorial/image10_2.png) 
 
8�DScripts/CarSimSample.cs��Vehicle�փh���b�O���h���b�v���܂�
![Image11](Img/Tutorial/image11.png) 

9.�h���b�v��CarSim�𗧂��グ�܂��B�����グ�̍ۂ�CarSim Solver for Windows�Ƀ`�F�b�N�����Ă��邱�Ƃ��m�F���܂��B

![Image12](Img/Tutorial/image12.png) 
 
 
10�D�G�f�B�^��Ŏ��s���܂��B�����Vehicle��GameObject���A�C�h�����O�ő���o���܂��B����o������Game��ʂŏ㉺���E�L�[�������ƃA�N�Z���E�u���[�L�E�X�e�A�����O���X�V���ꑖ��܂��B�m�F�ł������~���܂��B

![Image12_2](Img/Tutorial/image12_2.png) 

11�D�����ɁA�ԗւ�ǉ����Ă����܂��BVehicle��I��������ԂŉE�N���b�N��GameObject->Create Empty�������܂��B�ǉ����ꂽGameObject��WheelFL�Ɩ��̕ύX���A������E�N���b�N��GameObject->3D Object->Cylindar��I�����܂�.

![Image13](Img/Tutorial/image13.png)  

Cylindar�ɑ΂���Rotaion90,0,90, Scale0.65,0.1,0.65��ݒ肵�܂��B

![Image13_2](Img/Tutorial/image13_2.png)  

�܂����̍ۂ�Capsel Collidar�𖳌��ɂ��܂��B
![Image14](Img/Tutorial/image14.png)  

WheelFL��CarSim�Őݒ肳��Ă���z�C�[���ʒu�Ɉړ����܂��B

![Image15](Img/Tutorial/image15.png)   
![Image15_2](Img/Tutorial/image15_2.png)

12�DWheelFL��3������WheelFR,WheelRL,WheelRR�Ƃ��Ă��ꂼ��̈ʒu�Ɉړ������܂�

![Image16](Img/Tutorial/image16.png)  
 
13�DVehicle��I�������ۂ�Inspector��CarSimSample�R���|�l���g�Ŋe�^�C����Transform�ւ̎Q�Ƃ�����̂ł��ꂼ����h���b�O���Đݒ肵�܂��B

![Image17](Img/Tutorial/image17.png)  

14�D�ēx���s����ƃ^�C���̈ʒu�E�p�x���L�[���͂ɍ��킹�čX�V����܂��B

