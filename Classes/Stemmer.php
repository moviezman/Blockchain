<?php
class Stemmer
{
    public $code;
	public $heeftStem;

	function __construct(){
		$this->code = "";
		$this->heeftStem = true;
	}
	
    function Stem() {
		if($this->heeftStem == true) {
			echo  "Ik stem";
			$this->heeftStem = false;
		} else {
			echo "Ik heb al gestemd";
		}
    }
}
?>