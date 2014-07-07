#pragma strict

    var maxDist = 30;
var speed = 40.0;

function Update () {
	light.range = Mathf.PingPong(Time.time * speed, maxDist);
	//flicker();
}



 
 
    
