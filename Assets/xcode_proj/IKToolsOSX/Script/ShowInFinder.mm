//
//  ShowInFinder.m
//  IKToolsOSX
//
//  Created by 王池 on 2021/5/6.
//

#include "IKOSXToolsHeader.pch"
extern "C"
{
   void ShowInFinder(const unsigned char * paths)
   {
       const char * cPaths = (const char *)paths;
       NSString * pathsStr = [NSString stringWithUTF8String:cPaths];
       NSArray * splitResult = [pathsStr componentsSeparatedByString:[NSString stringWithFormat:@"%c",28]];
       [[NSWorkspace sharedWorkspace] activateFileViewerSelectingURLs:splitResult];
   }
}
