IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonXApplicationRoleDelete')
	BEGIN
		PRINT 'Dropping Procedure PersonXApplicationRoleDelete'
		DROP  Procedure  PersonXApplicationRoleDelete
	END

GO

PRINT 'Creating Procedure PersonXApplicationRoleDelete'
GO


/******************************************************************************
**		File: 
**		Name: PersonXApplicationRole_Delete
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
CREATE Procedure dbo.PersonXApplicationRoleDelete
(
		@PersonId			INT = NULL
	,	@ApplicationRoleId	INT = NULL
)
AS
BEGIN
		DELETE
		FROM	dbo.PersonXApplicationRole
		WHERE	(
					((PersonId IS NOT NULL) AND (PersonId = @PersonId))
					OR
					((@ApplicationRoleId IS NOT NULL) AND (ApplicationRoleId = @ApplicationRoleId))
				)
END

GO

