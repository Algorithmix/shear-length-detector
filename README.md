shear-length-detector
=====================
This repo contains a console program with a function to determine the length of the shear at a particular point.

This function works by searching for a black text or white paper.  whichever comes first is taken to indicate
the end of the shear.

There are 2 parameters and these are the color difference between 255+255+255 and text/paper.  The color difference
parameters are used to determine whether the current pixel is part of black text, part of the paper, or part of the shear.

The function returns the x coordinate of the END of the shearing.

The main function shows an example usage of the function.  This main simply draws blue dots at the edge of the shear
throughout the shred.

ASSUMPTIONS MADE BY FUNCTION:
  -text is black
  -paper is white
