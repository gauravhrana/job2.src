IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeDelete')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeDelete'
	DROP  Procedure  ActivitySubTypeDelete
END
GO

PRINT 'Creating Procedure ActivitySubTypeDelete'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySubTypeDelete
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

CREATE Procedure dbo.ActivitySubTypeDelete
(
	    @ActivitySubTypeId	INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL
	,	@SystemEntityType	VARCHAR(50)	= 'ActivitySubType'	
)
AS
BEGIN
	DELETE	dbo.ActivitySubType
	WHERE	ActivitySubTypeId = @ActivitySubTypeId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

