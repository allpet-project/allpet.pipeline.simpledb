## simpledb Module 协议格式定义##
>
|version| Method| Len   |                               command                             |  
                        |        基本类型          |     |              object              |   
|___V___|___M___|___L___|___T___|_________D________|.....|___T___|___L___|_________D________|  
|-1byte-|-2byte-|-4byte-|-4byte-|按照实际的类型计算|.....|-4byte-|-4byte-|按照实际的类型计算|  

- V:协议本  
  M:simpledb的方法，例如delete、put、get等  
  L:后面command的总长度  
  
  #Command#  
  
  #基本类型  
-
   T:调用simpledb的方法的参数的类型，例如：Int32、Double、byte等  
   D:参数的值
   
   #object(String和byte[])
_  
   T:调用simpledb的方法的参数的类型，例如：String和byte[]  
   L:后面参数值的长度
   D:参数的值


