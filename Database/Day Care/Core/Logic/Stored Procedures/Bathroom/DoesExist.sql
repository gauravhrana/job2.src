IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='BathroomDoesExist')
BEGIN
	PRINT 'Dropping Procedure BathroomDoesExist'
	DROP  Procedure  BathroomDoesExist
END
GO

PRINT 'Creating Procedure BathroomDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: BathroomDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.BathroomDoesExist
(
		@StudentId				INT				= NULL
	,	@ApplicationId			INT		
	,	@DiaperStatusId			INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Bathroom'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Bathroom a
	WHERE		a.StudentId			=	@StudentId
	AND			a.DiaperStatusId	=	@DiaperStatusId	
	AND			a.ApplicationId 	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

