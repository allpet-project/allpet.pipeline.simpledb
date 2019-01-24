## simpledb Module 协议格式定义##
>
|&nbsp;&nbsp;&nbsp; version &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; Method &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Len &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    command        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;基本类型&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     object     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|   
|_ _ _ V_ _ _ |_ _ _M_ _ _ |_ _ _L_ _ _ |_ _ _T_ _ _ |_ _ _ _ _ _ _D_ _ _  _ _| . . . . . |_ _ _T_ _ _ |_ _ _L_ _ _ |_ _ _ _ _ _ _ _ _D_ _ _ _ _ _ _ _ |  
|- -1byte- - |- -2byte- -|- -4byte--|- -4byte--|   按照实际的类型计算   | . . . . . |- -4byte- - |- -4byte- - |        按照实际的类型计算        |  

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


