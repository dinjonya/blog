if [ ! -d "Publish" ]; then
    echo 'Publish not exists,please look your current path'  
    exit 0
fi



#进入发布文件夹  清空发布文件夹
cd Publish
rm -rf *
#退出
cd ..

echo -e "\033[47;35m apiblog \033[0m" 
cd apiblog
dotnet build
dotnet publish -o ../Publish/apiblog
cd ..


echo -e "\033[47;35m Authen \033[0m" 
cd Authen
dotnet build
dotnet publish -o ../Publish/Authen
cd ..


echo -e "\033[47;35m uiblog \033[0m" 
cd uiblog
dotnet build
dotnet publish -o ../Publish/uiblog
cd ..


echo -e "\033[47;35m Publish \033[0m" 
cd Publish



cd apiblog
zip -r ./../apiblog.zip ./*
cd ..

cd Authen
zip -r ./../Authen.zip ./*
cd ..

cd uiblog
zip -r ./../uiblog.zip ./*
cd ..