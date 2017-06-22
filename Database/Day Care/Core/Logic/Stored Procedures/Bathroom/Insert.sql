IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomInsert')
BEGIN
	PRINT 'Dropping Procedure BathroomInsert'
	DROP  Procedure  BathroomInsert
END
GO

PRINT 'Creating Procedure BathroomInsert'
GO

/******************************************************************************
**		File: 
**		Name: BathroomInsert
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
CREATE Procedure dbo.BathroomInsert
(
		@BathroomId		    INT  
	,	@ApplicationId		INT
	,	@StudentId			INT
	,	@TimeIn				DATETIME
	,	@DiaperStatusId		INT
	,	@DiaperCream		VARCHAR(50)
	,	@PottyStatus		VARCHAR(50)
	,	@TeacherId			INT
	,	@TeacherNotes	    VARCHAR(50)	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Bathroom'	
)
AS
BEGIN	
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BathroomId OUTPUT

	INSERT INTO dbo.Bathroom
	(
			 BathroomId
		,	 ApplicationId
		,	 StudentId
		,	 TimeIn
		,	 DiaperStatusId
		,	 DiaperCream
		,	 PottyStatus
		,	 TeacherId
		,	 TeacherNotes
	)
	VALUES
	(
			@BathroomId
		,	@ApplicationId
		,	@StudentId
		,	@TimeIn
		,	@DiaperStatusId
		,	@DiaperCream
		,	@PottyStatus
		,	@TeacherId
		,	@TeacherNotes
	)
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
