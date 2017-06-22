IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SetUpConfigurationList')
BEGIN
	PRINT 'Dropping Procedure SetUpConfigurationList'
	DROP  Procedure  dbo.SetUpConfigurationList
END
GO

PRINT 'Creating Procedure SetUpConfigurationList'
GO

/******************************************************************************
**		File: 
**		EntityName: SetUpConfigurationList
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

CREATE Procedure dbo.SetUpConfigurationList
(
		@ApplicationId			INT			= NULL				
)
AS
BEGIN

	SELECT	a.SetupConfigurationId
		,	a.ApplicationId			
		,	a.EntityName			
		,	a.ConnectionKeyName	
	FROM	dbo.SetUpConfiguration a
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.SetupConfigurationId				ASC

END		
GO