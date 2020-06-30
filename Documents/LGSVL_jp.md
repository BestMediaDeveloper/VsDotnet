# VS UnityをLGSVLSimulatorへの適用

![Image0](Img/LGSVL/image0.jpg) 

1.IVehicleDynamics.csを編集

![Image1](Img/LGSVL/image1.png) 

Rigidbody RB { get; }

が存在するが上記はCarSimでは対応できないため、RigidBodyの使用している速度・各軸の速度ベクトル、角速度ベクトルを取得するようにする。

それにより上記に合わせて修正すべき点を修正。修正内容に関しては割愛します。RigidBodyの変数を使用しているものをInterfaceのものに使用するだけなので簡単です。

2.LGSVLシミュレータに最新のCarSimのスクリプトを追加

詳細はチュートリアルを参照

その際にSamplesにLGSVL用のスクリプトがあるのでそれをAssets/Scripts/Dynamics/Examplesに追加

![Image2](Img/LGSVL/image2.png) 

3.車両への適用
今回対応させる車両はJaguar2015XEが近しいのでそれを使用する。

https://github.com/lgsvl/Jaguar2015XE

![Image3](Img/LGSVL/image3.png) 

Jaguar2015XEのプレハブをダブルクリックし修正。

![Image4](Img/LGSVL/image4.png) 

VehicleSMIコンポーネントを削除

![Image5](Img/LGSVL/image5.png) 

CarSimSMIを追加その際にCarSimComponentも追加される。

![Image6](Img/LGSVL/image6.png) 


CarSimSMIに対してタイヤのTransformを設定する。

![Image7](Img/LGSVL/image7.png) 

CarSimとLGSVLのボディの原点が異なるので、CarSimComponentでBodyOffsetを-1.5を入力する

![Image8](Img/LGSVL/image8.png) 

Rayによる判定はDefaultのみに設定しているが、使用状況に合わせて設定。

![Image9](Img/LGSVL/image9.png) 

Jaguar2015XEのRigitBodyは影響は受けないのでIsKinematicをチェックする

![Image10](Img/LGSVL/image10.png) 

AssetBundlesをビルドし、実行する。

![Image11](Img/LGSVL/image11.png) 

*問題点としてはまだLGSVLのPhysic Materialの摩擦係数の変更がわからず、スリップしてしまう。


