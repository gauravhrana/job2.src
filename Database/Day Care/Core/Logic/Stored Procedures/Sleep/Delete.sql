IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepDelete')
BEGIN
	PRINT 'Dropping Procedure SleepDelete'
	DROP  Procedure  SleepDelete
END
GO

PRINT 'Creating Procedure SleepDelete'
GO

/******************************************************************************
**		File: 
**		Name: SleepDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SleepDelete
(
	    @SleepId	        INT
	,	@ApplicaitonId		INT		 = NULL
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL
	,   @SystemEntityType	VARCHAR(50)	= 'Sleep' 
)
AS
BEGIN
	DELETE	dbo.Sleep
	WHERE	SleepId = @SleepId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SleepId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

