<?php

header("Access-Control-Allow-Origin: *");

$attachment_location = "information.txt";
    if (file_exists($attachment_location)) {

        header($_SERVER["SERVER_PROTOCOL"] . " 200 OK");
        header("Cache-Control: public"); // needed for internet explorer
        header("Content-Type: text/plain");
        header("Content-Length:".filesize($attachment_location));
        header("Content-Disposition: attachment; filename=information.txt");
        readfile($attachment_location);
        die();        
    } else {
        die("Error: File not found.");
    } 

?>