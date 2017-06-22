/******************************************************************************
**		Name: Bathroom
*******************************************************************************/

EXEC dbo.Bathroom_Update @BathroomId = 102	,	@StudentId = 105    ,	@TimeIn = '12/23/2011'	,	@DiaperStatusId = 108   ,  @DiaperCream = 'Vanish'     ,  @PottyStatus = 'NotClean'   ,  @TeacherId = 111     ,  @TeacherNotes = 'Social'     
EXEC dbo.Bathroom_Update @BathroomId = 103	,	@StudentId = 106	,	@TimeIn = '12/24/2011'	,   @DiaperStatusId = 109   ,  @DiaperCream = 'Huggy'      ,  @PottyStatus = 'NotClean'   ,  @TeacherId = 112     ,  @TeacherNotes = 'Hindi'  
EXEC dbo.Bathroom_Update @BathroomId = 104	,	@StudentId = 107    ,	@TimeIn = '12/25/2011'	,	@DiaperStatusId = 110   ,  @DiaperCream = 'Cleanup'    ,  @PottyStatus = 'Clean'      ,  @TeacherId = 113     ,  @TeacherNotes = 'Computer'   

