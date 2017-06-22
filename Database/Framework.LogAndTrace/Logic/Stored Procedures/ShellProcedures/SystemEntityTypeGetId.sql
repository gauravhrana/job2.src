IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeGetId')
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
**		Desc: Thisd procedure is to create next SystemEntityTypeId.
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
**		Date: 09-22-05
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityName:
**		--------	--------			--------------------------------------
**    
*******************************************************************************/

CREATE PROCEDURE dbo.SystemEntityTypeGetId
(
		@SystemEntityType		VARCHAR(50)
	,	@SystemEntityTypeId		INT			= NULL	OUTPUT
)
AS
BEGIN		
	
	
	EXEC	TaskTimeTracker.dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

END
GO
