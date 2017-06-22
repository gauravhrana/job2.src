IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityTypeGetId')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeGetId'
	DROP  Procedure SystemEntityTypeGetId
END
GO

PRINT 'Creating Procedure SystemEntityTypeGetId'
GO


/******************************************************************************
**		File: 
**		Name: SystemEntityTypeGetId
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
CREATE PROCEDURE [dbo].[SystemEntityTypeGetId]
(
		@SystemEntityType		VARCHAR(50)
	,	@SystemEntityTypeId		INT			= NULL	OUTPUT
)
AS
BEGIN		

	EXEC Configuration.dbo.SystemEntityTypeGetId 
		@SystemEntityType	=	@SystemEntityType
	,	@SystemEntityTypeId =	@SystemEntityTypeId	OUTPUT
	
END