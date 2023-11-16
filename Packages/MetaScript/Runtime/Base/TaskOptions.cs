using System;

[Flags]
public enum TaskOptions
{

	Run  = 1,
	Wait = 2,
	// Ideally, it's up to the developer when a new scope is required.
	// We're just guessing by putting them in the most sensible places; i.e. task sets
	// Obvious use-cases like task sets (and wherever new scopes currently appear) could have this flag on by default
	// But the option to create a new scope for any task would be perfect.
//	NewScope = 4

}