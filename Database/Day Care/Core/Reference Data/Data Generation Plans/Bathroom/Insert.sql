/******************************************************************************
**		Name: Bathroom
*******************************************************************************/

EXEC dbo.Bathroom_Insert @BathroomId = 90	,	@StudentId = 93    ,	@TimeIn = '12/20/2011'	,	@DiaperStatusId = 96  ,  @DiaperCream = 'Colin'     ,  @PottyStatus = 'Clean'      ,  @TeacherId = 99     ,  @TeacherNotes = 'English'     
EXEC dbo.Bathroom_Insert @BathroomId = 91	,	@StudentId = 94	   ,	@TimeIn = '12/21/2011'	,   @DiaperStatusId = 97  ,  @DiaperCream = 'Ratty'     ,  @PottyStatus = 'NotClean'   ,  @TeacherId = 100    ,  @TeacherNotes = 'Maths'  
EXEC dbo.Bathroom_Insert @BathroomId = 92	,	@StudentId = 95	   ,	@TimeIn = '12/22/2011'	,	@DiaperStatusId = 98  ,  @DiaperCream = 'Chicky'    ,  @PottyStatus = 'Clean'      ,  @TeacherId = 101    ,  @TeacherNotes = 'Science'   

