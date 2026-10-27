# AutomaV2

**AutomaV2** is a rewrite of my [Automa](https://github/patrickcortez/Automa.git)  which is written in C#.

## Key Words:

The following are keywords in the AutomaV2 language:

	- Write
	- Read
	- Run
	- Let
	- If
	- Elif
	- Else
	- While
	- Func


## Program Structure

``` Automa v2

Write("Hello, World!"); # Write Instruction

Let x = 5; # Variable Declaration

If(x > 3) { # If Statement
	Write("x is greater than 3");
} Elif(x == 3) { # Elif Statement
	Write("x is equal to 3");
} Else { # Else Statement
	Write("x is less than 3");
}

While(x > 0) { # While Loop
	Write(x);
	Let x = x - 1;
}

```