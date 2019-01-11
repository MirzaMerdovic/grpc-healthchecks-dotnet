:: ProtoToolsVersion depends on the version of Google.Protobuf.Tools nuget package being referenced by the project.
SET ProtoToolsVersion=3.6.1
:: GrpcToolsVersion depends on the version of Grpc.Tools nuget package being referenced by the project.
SET GrpcToolsVersion=1.17.1

for %%P in (*.proto) do "%USERPROFILE%\.nuget\packages\Grpc.Tools\%GrpcToolsVersion%\tools\windows_x64\protoc.exe" %%P -I=. -I=.\bin\netcoreapp2.2 -I="%USERPROFILE%\.nuget\packages\google.protobuf.tools\%ProtoToolsVersion%\tools" --plugin=protoc-gen-grpc="%USERPROFILE%\.nuget\packages\Grpc.Tools\%GrpcToolsVersion%\tools\windows_x64\grpc_csharp_plugin.exe" --grpc_out . --csharp_out=. --csharp_opt=file_extension=.g.cs