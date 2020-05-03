<?php

 header("Access-Control-Allow-Origin: *");

//https://www.w3schools.com/php/php_file_upload.asp

$target_dir = "audio/";
$target_file = $target_dir . basename($_FILES["fileToUpload"]["name"]);
$uploadOk = 1;

if(isset($_POST["submit"])) {
    $uploadOk = 1;
}else{
    $uploadOk = 0;
}

if (move_uploaded_file($_FILES["fileToUpload"]["tmp_name"], $target_file)) {
    
    echo "The file ". basename( $_FILES["fileToUpload"]["name"]) . " has been uploaded.";
    $path = dirname(__FILE__).'/information.txt';
    $file_handle = fopen($path, 'a');
    fwrite($file_handle, basename($_FILES["fileToUpload"]["name"]) . "\n");
    fclose($file_handle);


} else {
    echo "Sorry, there was an error uploading your file.";
}

?>