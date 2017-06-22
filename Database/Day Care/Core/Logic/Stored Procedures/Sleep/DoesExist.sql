IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='SleepDoesExist')
BEGIN
	PRINT 'Dropping Procedure SleepDoesExist'
	DROP  Procedure  SleepDoesExist
END
GO

PRINT 'Creating Procedure SleepDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: SleepDoesExist
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

Create procedure dbo.SleepDoesExist
(
		@StudentId				INT				= NULL		
	,	@ApplicationId			INT						
	,	@Date					DATETIME		= NULL		
	,	@NapStart				DATETIME		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Sleep'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Sleep a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.Date				=	@Date
	AND			a.NapStart			=	@NapStart
	AND			a.ApplicationId		=	@ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Sleep'	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

