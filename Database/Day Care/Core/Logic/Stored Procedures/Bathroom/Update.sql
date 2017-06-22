IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomUpdate')
BEGIN
	PRINT 'Dropping Procedure BathroomUpdate'
	DROP  Procedure  BathroomUpdate
END
GO

PRINT 'Creating Procedure BathroomUpdate'

GO

/******************************************************************************
**		File: 
**		Name: BathroomUpdate
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
**		----------						-----------
**
**		Auth:
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.BathroomUpdate
(      
		@BathroomId		    INT	
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
	UPDATE	 dbo.Bathroom
	SET		 StudentId			=		@StudentId
		,	 TimeIn				=		@TimeIn
		,	 DiaperStatusId		=		@DiaperStatusId
		,	 DiaperCream		=		@DiaperCream
		,	 PottyStatus		=		@PottyStatus
		,	 TeacherId			=		@TeacherId
		,	 TeacherNotes		=		@TeacherNotes
	WHERE	BathroomId		    =		@BathroomId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

