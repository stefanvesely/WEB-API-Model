using System.Collections.Generic;
using try5000rpg.Models;
using System.Threading.Tasks;
using try5000rpg.DTOs.Characters;


namespace try5000rpg.Services.CharacterService
{
    public interface iCharService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int UserID);
        Task<ServiceResponse<GetCharacterDTO>> GetSingle (int id);
        Task<ServiceResponse<List<GetCharacterDTO>>> CreateSingle(AddCharacterDTO newSingle);

        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter (UpdateCharacterDTO updatedCharacter);

        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter (int iCharacterID);
    }
}