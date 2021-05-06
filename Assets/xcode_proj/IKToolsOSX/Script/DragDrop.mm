//
//  DragDrop.m
//  IKToolsOSX
//
//  Created by 王池 on 2021/3/25.
//

#include "IKOSXToolsHeader.pch"
extern "C"
{
bool InitializeDragDropComponent(draggingStatusChangedFunc draggingCallBack)
{
    NSView * mainView =[[[NSApplication sharedApplication] mainWindow] contentView];
    ReceiveFileView * fileView =  [[ReceiveFileView alloc] initWithFrame:CGRectMake(0,0,mainView.frame.size.width,mainView.frame.size.height)];
    [fileView SetupCallBacks:draggingCallBack];
    mainView.autoresizesSubviews = YES;
    fileView.autoresizingMask = NSViewWidthSizable|NSViewHeightSizable;
    [mainView addSubview:fileView];
    return true;
}
}

@implementation ReceiveFileView
{
    draggingStatusChangedFunc draggingStatusChanged;
}

-(instancetype)initWithFrame:(NSRect)frameRect
{
    self = [super initWithFrame:frameRect];
    if (self)
    {
        if (@available(macOS 10.13, *)) {
            [self registerForDraggedTypes:[NSArray arrayWithObjects:NSPasteboardTypeFileURL, nil]];
        } else {
            // Fallback on earlier versions
        }
    }
    return self;
}


-(NSDragOperation)draggingEntered:(id<NSDraggingInfo>)sender
{
    return [self processDraggingEvent:draggingStatusTypeEntered sender:sender]?NSDragOperationCopy:NSDragOperationNone;
}

-(NSDragOperation) draggingUpdated:(id<NSDraggingInfo>)sender
{
    return [self processDraggingEvent:draggingStatusTypeMoved sender:sender]?NSDragOperationCopy:NSDragOperationNone;
}

-(void) draggingExited:(id<NSDraggingInfo>)sender
{
    [self processDraggingEvent:draggingStatusTypeCanceled sender:sender];
}


-(BOOL)performDragOperation:(id<NSDraggingInfo>)sender{
    
    return [self processDraggingEvent:draggingStatusTypeEnded sender:sender];
}


-(BOOL) processDraggingEvent:(draggingStatusType) statusType sender:(id<NSDraggingInfo>)sender
{
    NSPasteboard *pboard = [sender draggingPasteboard];
    NSArray *classArray =[NSArray arrayWithObject:[NSURL class]];
    NSArray *arrayOfURLs = [pboard readObjectsForClasses:classArray options:nil];
    CGPoint focusPoint = [sender draggingLocation];
    
    if ([arrayOfURLs count]<1 || !draggingStatusChanged)
    {
        return false;
    }
    
    NSMutableArray* paths = [[NSMutableArray alloc]init];
    
    for (int i = 0; i< arrayOfURLs.count; i++)
    {
        [paths addObject:[[arrayOfURLs objectAtIndex:i] path]];
    }
    
    NSString* seperator = [NSString stringWithFormat:@"%c", 28];
    NSString * pathsStr = [paths componentsJoinedByString:seperator];
   
    return draggingStatusChanged((int)statusType,[pathsStr UTF8String],focusPoint.x,focusPoint.y);
    
}


-(void) SetupCallBacks:(draggingStatusChangedFunc) draggingStatus
{
    draggingStatusChanged = draggingStatus;
}



@end


