# PerformanceTest


## Design patterns:

* Object Pool

reusing ui elements instead of instantiating them again and again

* Factory

letting a factory class make instance of a class. here jenject generates it


* Dependency Injection

making a loose coupled code


## Code Conventions:
* Cohesive Code and SOlid

for example manipulating items are placed at their own class and private variables are writtern near their utility function. 

* Naming

* Commenting

* Short Function And Classes

* Using Di and interfaces for better encapsulation

* avoid hard code and place string and numbers in scriptableobjects behind interfaces or static classes

## Performance

* Pooling
(mentioned before)

* Atlas
place images in single assets to make lower draw calls
for less draw calls i had to remove some unused ui elements at same time. right now batched are between 9 and 18

* pagination
instead of showing 1k list, just paing and showing some of them at same time. 



## Things Could be better

* better naming

i changes names to follow rules but some of them are referenced in unity. rider changes them automatically

* using mvvm design pattern

seperate any UI related in oother class to make it fully testable


* using addressables and scriptable objects over direct referencing and json

at this scenario it would not change alot and also adds delay on async loading images. but could be usefull in general

## Plugins Used

no plugin used in main project. just zenject to resolve dependencies.
auto saver to save scenes automatically.