IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomList')
BEGIN
	PRINT 'Dropping Procedure BathroomList'
	DROP PROCEDURE BathroomList
END
GO

PRINT 'Creating Procedure BathroomList'
GO

/******************************************************************************
**		File: 
**		Name: BathroomList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.BathroomList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Bathroom'
)
AS
BEGIN
		SELECT	a.BathroomId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.TimeIn		
			,	a.DiaperStatusId
			,	a.DiaperCream
			,	a.PottyStatus
			,   a.TeacherId
			,	a.TeacherNotes
		FROM   dbo.Bathroom  a
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY BathroomId	ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
