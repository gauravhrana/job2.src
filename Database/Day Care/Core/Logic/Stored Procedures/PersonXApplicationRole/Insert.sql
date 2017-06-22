IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonXApplicationRoleInsert')
	BEGIN
		PRINT 'Dropping Procedure PersonXApplicationRoleInsert'
		DROP  Procedure  PersonXApplicationRoleInsert
	END

GO

PRINT 'Creating Procedure PersonXApplicationRoleInsert'
GO


/*********************************************************************************************
**		File: 
**		Name:PersonXApplicationRoleInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		09-25-2005	Arun				Added the If statement to generate JobApplicationId
**********************************************************************************************/

Create Procedure dbo.PersonXApplicationRoleInsert
(
		@PersonId			INT = NULL
	,	@ApplicationRoleId	INT = NULL
)
AS
BEGIN
		INSERT INTO dbo.PersonXApplicationRole
		(
				PersonId
			,	ApplicationRoleId
		)
		VALUES
		(		
				@PersonId
			,	@ApplicationRoleId
		)
END

GO

