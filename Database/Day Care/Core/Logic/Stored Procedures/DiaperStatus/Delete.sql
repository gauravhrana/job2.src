IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusDelete')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusDelete'
	DROP  Procedure  DiaperStatusDelete
END
GO

PRINT 'Creating Procedure DiaperStatusDelete'
GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusDelete
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

CREATE Procedure dbo.DiaperStatusDelete
(
		@DiaperStatusId  	INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME    = NULL
	,	@SystemEntityType	VARCHAR(50)	= 'DiaperStatus'
)
AS
 BEGIN
	DELETE	dbo.DiaperStatus
	WHERE	DiaperStatusId = @DiaperStatusId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DiaperStatusId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

