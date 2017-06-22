IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeList')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeList'
	DROP PROCEDURE ActivitySubTypeList
END
GO

PRINT 'Creating Procedure ActivitySubTypeList'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySubTypeList
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

CREATE Procedure dbo.ActivitySubTypeList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ActivitySubType'
)
AS
BEGIN
		SELECT	ActivitySubTypeId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.ActivitySubType 
		WHERE ApplicationId			 = ISNULL(@ApplicationId, ApplicationId)
		ORDER BY ActivitySubTypeId	ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
