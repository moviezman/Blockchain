<!DOCTYPE html>
<html>
<body>

<h1>My first PHP page</h1>

<?php
echo "Hello World!<br><br>";
include '/Classes/Stemmer.php';
$jaap = new Stemmer();
$jaap->Stem();
$jaap->Stem();
?> 

</body>
</html>