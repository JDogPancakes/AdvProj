UI Value Bar System:
	Health Bar, Mana Bar, whatever the name, is a bar.

Demonstration Scene Explain:
	A to reduce health
	D to reduce mana

How to use:
	1. Drag HealthBar, or ManaBar prefab on an Object
		or Set up prefab for other value bars with prefab ParentObject_ValueBarCanvas 
	
	2. Give an Object with a component that will provide info about Max Value, Current Value, and the Target Bar Component References.

		Example File:
			UI_Develop_Info.cs

		Example - Health Bar:
			Health Info:
				Current_Health : 100
				Max_Health: 100
				Health Bar Reference: Manually Pick from the component (Or set up auto search function as I do in the Start() of UI_Develop_Info.cs)

	3.  Assign Player Value in the Start() function
		Example:
			healthBar.SetUpPlayerValue(max_Health, currrent_Health);
        		manaBar.SetUpPlayerValue(max_Mana, currrent_Mana);
		IMPORTANT: DO NOT ASSIGN VALUE IN AWAKE(), UNLESS RESTRUCTURED EXECUTE ORDER