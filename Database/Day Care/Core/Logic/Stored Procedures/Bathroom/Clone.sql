IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomClone')
BEGIN
	PRINT 'Dropping Procedure BathroomClone'
	DROP  Procedure BathroomClone
END
GO

PRINT 'Creating Procedure BathroomClone'
GO

/*********************************************************************************************
**		File: 
**		Name: BathroomClone
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.BathroomClone
(
		@BathroomId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT						
	,	@StudentId				INT								
	,	@TimeIn					DATETIME						
	,	@DiaperStatusId			INT								
	,	@DiaperCream			VARCHAR(50)						
	,	@PottyStatus			VARCHAR(50)						
	,	@TeacherId				INT									
	,	@TeacherNotes			VARCHAR(50)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Bathroom'			
)

AS

BEGIN

	IF @BathroomId IS NULL OR @BathroomId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BathroomId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		=   ApplicationId
		,	@StudentId			=	StudentId
		,	@TimeIn				=	TimeIn
		,	@DiaperStatusId		=	DiaperStatusId
		,	@DiaperCream		=	DiaperCream
		,	@PottyStatus		=	PottyStatus
		,	@TeacherId			=	TeacherId
		,	@TeacherNotes		=	TeacherNotes				
	FROM	dbo.Bathroom
	WHERE	BathroomId			= @BathroomId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.BathroomInsert 
			@BathroomId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@StudentId			=	@StudentId
		,	@TimeIn				=	@TimeIn
		,	@DiaperStatusId		=	@DiaperStatusId
		,	@DiaperCream		=	@DiaperCream
		,	@PottyStatus		=	@PottyStatus
		,	@TeacherId			=	@TeacherId
		,	@TeacherNotes		=	@TeacherNotes
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
