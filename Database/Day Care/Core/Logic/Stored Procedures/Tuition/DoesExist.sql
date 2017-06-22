IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TuitionDoesExist')
BEGIN
	PRINT 'Dropping Procedure TuitionDoesExist'
	DROP  Procedure  TuitionDoesExist
END
GO

PRINT 'Creating Procedure TuitionDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TuitionDoesExist
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

Create procedure dbo.TuitionDoesExist
(
		@ApplicationId			INT						
	,	@StudentId				INT				= NULL		
	,	@TuitionDueDate			DATETIME		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
    ,	@SystemEntityType		VARCHAR(50)		= 'Teacher'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Tuition a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.TuitionDueDate	=	@TuitionDueDate
	AND			a.ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Tuition'	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

