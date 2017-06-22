IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusList')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusList'
	DROP PROCEDURE DiaperStatusList
END
GO

PRINT 'Creating Procedure DiaperStatusList'
GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusList
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

CREATE Procedure dbo.DiaperStatusList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DiaperStatus'
)
AS
BEGIN
		SELECT	DiaperStatusId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.DiaperStatus 
		WHERE ApplicationId			= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY DiaperStatusId	    ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO




