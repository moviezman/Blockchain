<?php
class Stemmer
{
    public $code = "";
	public $heeftStem = "True";

    function Stem() {
        echo  "Ik stem";
    }
}
$henk= new Stemmer();
$henk->Stem();
?>