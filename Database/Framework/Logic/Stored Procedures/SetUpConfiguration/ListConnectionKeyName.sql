IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SetUpConfigurationListConnectionKeyName')
BEGIN
	PRINT 'Dropping Procedure SetUpConfigurationListConnectionKeyName'
	DROP  Procedure  dbo.SetUpConfigurationListConnectionKeyName
END
GO

PRINT 'Creating Procedure SetUpConfigurationListConnectionKeyName'
GO

/******************************************************************************
**		File: 
**		EntityName: SetUpConfigurationListConnectionKeyName
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SetUpConfigurationListConnectionKeyName
(
		@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'SetUpConfiguration'
)
AS
BEGIN

	SELECT DISTINCT a.ConnectionKeyName 
	FROM dbo.SetUpConfiguration a

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO