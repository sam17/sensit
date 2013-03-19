/**
 * @file comdef.h
 *
 * @brief This file contains the common definitions used by the entire project
 *
 * @date Created: 6/28/2011 3:24:22 PM
 *
 * @author Bittu Sarkar
 */

#ifndef COMDEF_H
#define COMDEF_H

#ifdef DEFINE_GLOBALS
#define EXTERN
#else
#define EXTERN extern
#endif

/************************************************************************/
/* TYPE DECLARATIONS                                                    */
/************************************************************************/
typedef enum Result
{
  Success,
  Failure
} Result;

#endif // COMDEF_H
